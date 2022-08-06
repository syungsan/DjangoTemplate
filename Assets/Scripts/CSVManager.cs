using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVManager
{
    //��؂蕶��
    public char delim = ',';

    // CSV�t�@�C����ǂݍ���
    public List<string[]> CSVRead(string dataDir, string dataFile)
    {
        //�t�@�C���p�X���\�z
        var filePath = dataDir + "/" + dataFile;

        //�f�B���N�g�������邩
        if (System.IO.Directory.Exists(dataDir))
        {
            //�w�肵���p�X�̃t�@�C�����Ȃ���
            if (!System.IO.File.Exists(filePath))
            {
                //�t�@�C���쐬
                System.IO.File.Create(filePath);
                return new List<string[]>();
            }
        }
        else
        {
            //�f�B���N�g���쐬
            System.IO.Directory.CreateDirectory(dataDir);

            //�t�@�C���쐬
            System.IO.File.Create(filePath);
            return new List<string[]>();
        }

        List<string[]> data = new List<string[]>();

        using (var dataText = new StreamReader(filePath))
        {
            while (dataText.Peek() > -1)
            {
                string line = dataText.ReadLine().Replace("\\n", "\n");
                data.Add(line.Split(delim));
            }
            return data;
        }
    }

    // CSV�t�@�C���ɏ�������
    public void CSVWrite(List<string[]> writeDatas, string writeDataPath="write_data.csv", bool isAppend=false)
    {
        try
        {
            using (var sw = new System.IO.StreamWriter(writeDataPath, append: isAppend))
            {
                foreach (var data in writeDatas)
                {
                    sw.WriteLine(string.Join(",", data));
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public List<string[]> CSVReadFromResource(string resourcePath)
    {
        List<string[]> csvs = new List<string[]>();

        TextAsset rawText = Resources.Load(resourcePath) as TextAsset;
        StringReader reader = new StringReader(rawText.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine().Replace("\\n", "\n");
            csvs.Add(line.Split(delim)); // ���X�g�ɓ����
        }

        return csvs;
    }
}
