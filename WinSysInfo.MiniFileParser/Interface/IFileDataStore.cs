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

        /// <summary>
        /// Store the parent child relationship
        /// </summary>
        IDictionary ParentChild { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Get the list data
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        List<LayoutModel<TStruct>> GetListData<TStructId, TStruct>(TStructId enumVal)
            where TStruct : struct;

        /// <summary>
        /// Get the data
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="enumVal"></param>
        /// <returns></returns>
        LayoutModel<TStruct> GetData<TStructId, TStruct>(TStructId enumVal)
            where TStructId : struct
            where TStruct : struct;

        /// <summary>
        /// Set the data
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="enumVal"></param>
        /// <param name="modelList"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        void SetData<TStructId, TStruct>(TStructId enumVal, LayoutModel<TStruct> modelobj,
            TStructId? enumParent = null, int position = -1)
            where TStructId : struct
            where TStruct : struct;

        /// <summary>
        /// Set List of data
        /// </summary>
        /// <typeparam name="TStructId"></typeparam>
        /// <typeparam name="TStruct"></typeparam>
        /// <param name="enumVal"></param>
        /// <param name="modelobjList"></param>
        /// <param name="enumParent"></param>
        /// <param name="position"></param>
        void SetListData<TStructId, TStruct>(TStructId enumVal, List<LayoutModel<TStruct>> modelobjList,
            TStructId? enumParent = null, int position = -1)
            where TStructId : struct
            where TStruct : struct;

        /// <summary>
        /// Delete the model
        /// </summary>
        /// <param name="enumVal"></param>
        void Delete<TStructId>(TStructId enumVal) where TStructId : struct;

        #endregion Methods
    }
}
