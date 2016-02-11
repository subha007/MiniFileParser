using System;
using System.Runtime.InteropServices;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// The main layout class model which wraps the layout raw structure
    /// </summary>
    /// <typeparam name="TLayoutType"></typeparam>
    public class LayoutModel<TLayoutType> where TLayoutType : struct
    {
        #region Properties

        /// <summary>
        /// Get or set the layout type
        /// </summary>
        public EnumPEStructureId LayoutType { get; set; }

        /// <summary>
        /// Get or set the start position in the File (relative to file)
        /// </summary>
        public ulong StartPositionInFile { get; set; }

        /// <summary>
        /// Get or set the end position in the File (relative to file)
        /// </summary>
        public ulong EndPositionInFile { get; set; }

        /// <summary>
        /// Get or set the main data object
        /// </summary>
        protected TLayoutType? actualData;
        public TLayoutType Data 
        {
            get 
            {
                return actualData.Value;
            } 
        }

        #endregion Properties

        #region Constructor

        public LayoutModel() { }

        /// <summary>
        /// Shallow copy
        /// </summary>
        /// <param name="obj"></param>
        public LayoutModel(LayoutModel<TLayoutType> obj)
        {
            this.LayoutType = obj.LayoutType;
            this.actualData = obj.Data;
        }

        /// <summary>
        /// Shallow copy
        /// </summary>
        /// <param name="obj"></param>
        public LayoutModel(TLayoutType obj)
        {
            this.actualData = obj;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Set the data
        /// </summary>
        /// <typeparam name="TLayoutType"></typeparam>
        /// <param name="model"></param>
        public void SetData(TLayoutType model)
        {
            this.actualData = model;
        }

        /// <summary>
        /// Set the data
        /// </summary>
        /// <typeparam name="object"></typeparam>
        /// <param name="model"></param>
        public void SetData(object model)
        {
            this.actualData = (TLayoutType)model;
        }

        /// <summary>
        /// Get the offset of a field in  structure
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static long GetOffset(string fieldName)
        {
            IntPtr offset = Marshal.OffsetOf<TLayoutType>(fieldName);
            return offset.ToInt64();
        }

        /// <summary>
        /// Get the size of the structure
        /// </summary>
        public static uint DataSize
        {
            get
            {
                return (uint)Marshal.SizeOf(typeof(TLayoutType));
            }
        }

        #endregion Methods
    }
}
