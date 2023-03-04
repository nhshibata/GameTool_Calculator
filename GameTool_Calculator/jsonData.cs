using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

//--- 参照追加
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace GameTool_Calculator
{
    [DataContract]
    abstract class jsonData
    {
        // 保存先
        private string filePath;

        public string FilePath { get => filePath; set => filePath = value; }

        public jsonData()
        {
        
        }

        public jsonData(string path)
        {
            FilePath = path;
        }

        public void Write()
        {
            if (FilePath == null)
            {
                MessageBox.Show("保存名が設定されていません");
                return;
            }

            // 拡張子が付いているか確認
            // なければ付与
            if(!FilePath.Contains(".json"))
            {
                FilePath += ".json";
            }

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(GetType());

            using (MemoryStream stream = new MemoryStream())
            {
                serializer.WriteObject(stream, this);
                File.WriteAllBytes(FilePath, stream.ToArray());
                stream.Close();
            }
        }

        public void Read()
        {
            if (FilePath == null)
                MessageBox.Show("読み込み先が存在しません");

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(GetType());
            byte[] bytes = Encoding.UTF8.GetBytes(File.ReadAllText(FilePath));

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                object json = null;
                json = serializer.ReadObject(stream);
                ReadCallBack(json);
                stream.Close();
            }
        }


        protected abstract void ReadCallBack(object json);

    }
}
