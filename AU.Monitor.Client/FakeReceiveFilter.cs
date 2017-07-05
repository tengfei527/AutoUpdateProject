using AU.Monitor.Client.Extensions;
using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AU.Monitor.Client
{
    public class FakeReceiveFilter : TerminatorReceiveFilter<StringPackageInfo>
    {
        public System.Text.Encoding MessageEncoding { get; private set; }
        public FakeReceiveFilter(System.Text.Encoding m_Encoding, string terminator = "\r\n")
            : base(m_Encoding.GetBytes(terminator))
        {
            this.MessageEncoding = m_Encoding;
        }

        public override StringPackageInfo ResolvePackage(IBufferStream bufferStream)
        {
            var buffer = bufferStream.Read();
            string msg = MessageEncoding.GetString(buffer);
            return new StringPackageInfo(msg.Substring(0, msg.Length - 2), new BasicStringParser(":", ","));
        }
    }
}
