using System.Collections;
using System.Collections.Generic;
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

        /// <summary>
        /// Store the parent child relationship
        /// </summary>
        protected Dictionary<T1StructId, StateNode<T1StructId>> parentChild;
        public IDictionary ParentChild { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Base construction
        /// </summary>
        public FileDataStore()
        {
            this.FileData = new Dictionary<T1StructId, object>();
            this.LayoutOrder = new List<T1StructId>();
            this.ParentChild = new Dictionary<T1StructId, StateNode<T1StructId>>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Get the data
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public LayoutModel<TStruct> GetData<TStructId, TStruct>(TStructId enumVal)
            where TStructId : struct
            where TStruct : struct
        {
            LayoutModel<TStruct> model = null;
            if (this.FileData.Contains(enumVal) == true)
                model = this.FileData[enumVal] as LayoutModel<TStruct>;

            return model;
        }

        /// <summary>
        /// Get the list data
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<LayoutModel<TStruct>> GetListData<TStructId, TStruct>(TStructId enumVal)
            where TStruct : struct
        {
            List<LayoutModel<TStruct>> model = null;
            if (this.FileData.Contains(enumVal) == true)
                model = this.FileData[enumVal] as List<LayoutModel<TStruct>>;

            return model;
        }

        /// <summary>
        /// Set the data list
        /// </summary>
        /// <typeparam name="modelobj"></typeparam>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public void SetData<TStructId, TStruct>(TStructId enumVal, LayoutModel<TStruct> modelobj,
            TStructId? enumParent = null, int position = -1)
            where TStructId : struct
            where TStruct : struct
        {
            if (this.FileData.Contains(enumVal) == true)
                this.FileData[enumVal] = modelobj;
            else
                this.FileData.Add(enumVal, modelobj);

            if (position >= 0)
            {
                int fIndex = this.LayoutOrder.IndexOf(enumVal);
                if (fIndex == position)
                {
                    if (fIndex >= 0)
                        this.LayoutOrder.Insert(position, enumVal);
                    else
                        this.LayoutOrder.Add(enumVal);
                }
            }

            if (enumParent != null && enumParent.HasValue == true)
            {
                SetRelation(enumVal, enumParent.Value);
            }
        }

        /// <summary>
        /// Set the data list
        /// </summary>
        /// <typeparam name="modelobj"></typeparam>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        public void SetListData<TStructId, TStruct>(TStructId enumVal, List<LayoutModel<TStruct>> modelobjList,
            TStructId? enumParent = null, int position = -1)
            where TStructId : struct
            where TStruct : struct
        {
            if (this.FileData.Contains(enumVal) == true)
                this.FileData[enumVal] = modelobjList;
            else
                this.FileData.Add(enumVal, modelobjList);

            if (position >= 0)
            {
                int fIndex = this.LayoutOrder.IndexOf(enumVal);
                if (fIndex == position)
                {
                    if (fIndex >= 0)
                        this.LayoutOrder.Insert(position, enumVal);
                    else
                        this.LayoutOrder.Add(enumVal);
                }
            }

            if (enumParent != null && enumParent.HasValue == true)
            {
                SetRelation(enumVal, enumParent.Value);
            }
        }

        /// <summary>
        /// Set the relationship of parent child
        /// </summary>
        /// <param name="enumId"></param>
        /// <param name="enumParent"></param>
        public void SetRelation<TStructId>(TStructId enumId, TStructId enumParent)
        {
            if (this.ParentChild.Contains(enumParent) == false)
            {
                StateNode<TStructId> nodeParent = new StateNode<TStructId>();
                nodeParent.Id = enumParent;
                this.ParentChild.Add(enumParent, nodeParent);
            }

            if (((StateNode<TStructId>)this.ParentChild[enumParent]).Children.Contains(enumId) == false)
                ((StateNode<TStructId>)this.ParentChild[enumParent]).Children.Add(enumId);
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
