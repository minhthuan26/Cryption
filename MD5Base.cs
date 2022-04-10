using System.Text;

namespace QuanLyThuVien.Encryption
{
    public static class MD5Base
    {

        public static string Decode(this byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString("x2"));

            return result.ToString();
        }
    }
}