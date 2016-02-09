using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WinSysInfo.MiniFileParser.Model
{
    [Serializable]
    public class OptHeaderDataDirectoriesImageOnly : 
                Dictionary<EnumPEStructureId, OptionalHeaderDataDirImageOnlyLayoutModel>
    {
        #region Constructors

        /// <summary>
        /// Default Constructors
        /// </summary>
        public OptHeaderDataDirectoriesImageOnly() { }

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected OptHeaderDataDirectoriesImageOnly(SerializationInfo info, StreamingContext context)
            :base(info, context)
        { }

        #endregion Constructors
    }
}
