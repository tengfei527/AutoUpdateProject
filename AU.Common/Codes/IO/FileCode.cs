/*----------------------------------------------------------------
        // Copyright (C) 2007 L3'Studio
        // ��Ȩ���С� 
        // �����ߣ�L3'Studio�Ŷ�
        // �ļ�����FileCode.cs
        // �ļ������������漰���ļ������ָ��ļ�ָ���ࡣ
//----------------------------------------------------------------*/

using System;
using System.IO;

namespace AU.Common.Codes
{
    /// <summary>
    /// �ļ�ָ����
    /// </summary>
    [Serializable]
    public class FileCode : BaseCode
    {
        private string fileName;
        private string savePath;
        private byte[] mbyte;
        /// <summary>
        /// �ļ��ֽ���
        /// </summary>
        private long fileLength;
        /// <summary>
        /// �ļ����ֽڿ�
        /// </summary>
        public byte[] Mbyte
        {
            get { return mbyte; }
            set { mbyte = value; }
        }
        /// <summary>
        /// ����·��
        /// </summary>
        public string SavePath
        {
            get { return savePath; }
            set { savePath = value; }
        }
        /// <summary>
        /// �ļ�ָ����Ĺ��캯��
        /// </summary>
        /// <param name="fileName"></param>
        public FileCode(string fileName)
        {
            this.fileName = fileName;
        }
        /// <summary>
        /// ��ȡ�ļ�
        /// </summary>
        public void readFile()
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            fileLength = fileStream.Length;
            mbyte = new byte[fileLength];
            int m = 0;
            int startmbyte = 0;
            int allmybyte = (int)fileLength;
            do
            {
                m = fileStream.Read(mbyte, startmbyte, allmybyte);
                startmbyte += m;
                allmybyte -= m;

            } while (m > 0);
            fileStream.Close();
        }
        /// <summary>
        ///  �����ļ�
        /// </summary>
        public void SaveFile()
        {
            try
            {
                FileStream output = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write);
                output.Write(mbyte, 0, (int)fileLength);
                output.Close();
            }
            catch (Exception exp)
            {
                System.Windows.Forms.MessageBox.Show(exp.ToString());
            }
        }
    }
}
