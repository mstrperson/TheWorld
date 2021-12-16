using System;
using System.Collections.Generic;

namespace TheWorld
{
    public interface IInteractable
    {
        /// <summary>
        /// List of accepted command words and actions
        /// </summary>
        public Dictionary<string, Action<string[]>> Interactions { get; }
    }
}