using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Globalization;


public static class BSA
{
    public static void BSA_unpack(string filepath)
    {
        BinaryReader reader = new BinaryReader(File.Open(filepath, FileMode.Open));
        //header
        reader.ReadInt16();//pad
        short files_count = reader.ReadInt16();
        reader.ReadInt32();//unk
        short frame_x = reader.ReadInt16();
        short frame_y = reader.ReadInt16();
        reader.ReadBytes(40);//pad
        short[] x = new short[files_count];
        short[] y = new short[files_count];
        int[] file_size = new int[files_count];

        for (int i = 0; i < files_count; i++)
        {
            x[i] = reader.ReadInt16();
            y[i] = reader.ReadInt16();
            file_size[i] = reader.ReadInt32();
            reader.ReadInt32();//pad
        }
        //data
        for (int j = 0; j < files_count; j++)
        {
            byte[] buff = reader.ReadBytes(file_size[j]);
            File.WriteAllBytes("BSdata_" + j.ToString() + "_"+x[j].ToString()+"x"+y[j].ToString()+".bs", buff);
        }
        reader.Close();
    }
}
