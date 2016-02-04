using System.Collections;
using System.Collections.Generic;
using WinSysInfo.MiniFileParser.Model;

namespace WinSysInfo.MiniFileParser.Interface
{
    public interface IFileDataStore
    {
        #region Properties

        /// <summary>
        /// Data mapping
        /// </summary>
        IDictionary FileData { get; set; }

        /// <summary>
        /// Store the layout order
        /// </summary>
        IList LayoutOrder { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Get the data
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        object GetData<TStructId>(TStructId enumVal)
            where TStructId : struct;

        /// <summary>
        /// Get the data
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        object GetData(int index);

        /// <summary>
        /// Set the data
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="enumVal"></param>
        /// <param name="modelList"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        void SetData<TStructId>(TStructId enumVal, object modelList,
            int position = -1)
            where TStructId : struct;

        /// <summary>
        /// Delete the model
        /// </summary>
        /// <param name="enumVal"></param>
        void Delete<TStructId>(TStructId enumVal) where TStructId : struct;

        #endregion Methods
    }
}
