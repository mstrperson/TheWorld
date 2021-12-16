using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld
{
    /// <summary>
    /// A generic game Exception within TheWorld.
    /// </summary>
    public class WorldException : Exception
    {
        /// <summary>
        /// What object had this exception
        /// </summary>
        public object Sender { get; }
        
        public Type SenderType { get; }


        /// <summary>
        /// An Exception relating directly to a part of TheWorld.
        /// The "sender" parameter allows you to pass back to the programmer
        /// what thing in TheWorld caused this exception to be thrown.
        ///
        /// There might even be some clever ways that you can use this!
        /// </summary>
        /// <param name="message"></param>
        /// <param name="sender"></param>
        /// <param name="innerException"></param>
        public WorldException(string message, object sender = null, Type senderType = null, Exception innerException = null)
            : base(message, innerException)
        {
            Sender = sender;
            SenderType = senderType;
        }
    }
}
