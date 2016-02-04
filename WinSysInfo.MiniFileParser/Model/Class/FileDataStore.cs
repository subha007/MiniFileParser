using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinSysInfo.MiniFileParser.Interface;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The class to store file structure portion and navigate
    /// through
    /// </summary>
    public class FileDataStore<T1StructId> : IFileDataStore
    {
        #region Properties

        /// <summary>
        /// Data mapping
        /// </summary>
        protected Dictionary<T1StructId, object> fileData { get; set; }
        public IDictionary FileData { get; set; }

        /// <summary>
        /// Store the layout order
        /// </summary>
        protected List<T1StructId> layoutOrder { get; set; }
        public IList LayoutOrder { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Base construction
        /// </summary>
        public FileDataStore()
        {
            this.FileData = new Dictionary<T1StructId, object>();
            this.LayoutOrder = new List<T1StructId>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Get the data
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public object GetData<TStructId>(TStructId enumVal)
            where TStructId : struct
        {
            object model = null;
            if (this.FileData.Contains(enumVal) == true)
                model = this.FileData[enumVal];

            return model;
        }

        /// <summary>
        /// Get the data
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        public object GetData(int index)
        {
            if (!(index >= 0 && index < this.FileData.Count)) return null;

            T1StructId enumVal = (T1StructId)this.LayoutOrder[index];

            object model = null;
            if (this.FileData.Contains(enumVal) == true)
                model = this.FileData[enumVal];

            return model;
        }

        /// <summary>
        /// Set the data list
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public void SetData<TStructId>(TStructId enumVal, object modelList,
            int position = -1)
            where TStructId : struct
        {
            if (this.FileData.Contains(enumVal) == true)
                this.FileData[enumVal] = modelList;
            else
                this.FileData.Add(enumVal, modelList);

            int fIndex = this.LayoutOrder.IndexOf(enumVal);
            if (fIndex == position) return;
            if (fIndex >= 0)
                this.LayoutOrder.Insert(position, enumVal);
            else
                this.LayoutOrder.Add(enumVal);
        }

        /// <summary>
        /// Delete the model
        /// </summary>
        /// <param name="enumVal"></param>
        public void Delete<TStructId>(TStructId enumVal)
            where TStructId : struct
        {
            this.LayoutOrder.Remove(enumVal);
            if (this.FileData.Contains(enumVal) == true)
                this.FileData.Remove(enumVal);
        }

        #endregion Methods
    }
}
