using System.Collections.Generic;

namespace WinSysInfo.MiniFileParser.Model
{
    /// <summary>
    /// Tha class which represents a node
    /// </summary>
    /// <typeparam name="T1StructId"></typeparam>
    public class StateNode<T1StructId>
    {
        /// <summary>
        /// Get or set the main identifier value
        /// </summary>
        public T1StructId Id { get; set; }

        /// <summary>
        /// Get or set the parent
        /// </summary>
        public T1StructId ParentId { get; set; }

        /// <summary>
        /// get or set the children
        /// </summary>
        public List<T1StructId> Children { get; set; }

        public StateNode()
        {
            this.Children = new List<T1StructId>();
        }
    }
}
