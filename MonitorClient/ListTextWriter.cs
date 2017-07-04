using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MonitorClient
{
    internal class ListTextWriter : TextWriter
    {
        private ListBox listBox;
        private delegate void VoidAction();

        public ListTextWriter(ListBox box, long lastCount = 3000)
        {
            listBox = box;
            System.Threading.Tasks.Task t = new System.Threading.Tasks.Task(() =>
              {
                  while (true)
                  {
                      if (listBox.Items.Count >= lastCount)
                      {
                          VoidAction action = delegate
                          {
                              listBox.Items.Clear();
                          };
                          listBox.BeginInvoke(action);
                      }

                      System.Threading.Thread.Sleep(3000);
                  }
              });
            t.Start();
        }

        public override void Write(string value)
        {
            VoidAction action = delegate
            {
                listBox.Items.Insert(0, string.Format("[{0:HH:mm:ss}]{1}", DateTime.Now, value));
            };
            listBox.BeginInvoke(action);
        }

        public override void WriteLine(string value)
        {
            VoidAction action = delegate
            {
                listBox.Items.Insert(0, string.Format("[{0:HH:mm:ss}]{1}", DateTime.Now, value));
            };
            listBox.BeginInvoke(action);
        }

        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}