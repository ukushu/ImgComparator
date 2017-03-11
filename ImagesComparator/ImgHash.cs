using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ImagesComparator
{
    [Serializable]
    public class ImgHash
    {
        private readonly int _hashSide;

        private bool[] _hashData;
        public bool[] HashData
        {
            get { return _hashData; }
        }

        public Image Img
        {
            get
            {
                return Bitmap.FromFile(FilePath);
            }
        }

        public string FilePath { get; private set; }

        public string FileName
        {
            get
            {
                return Path.GetFileName(FilePath);
            }
        }

        public string FileLocation
        {
            get { return Path.GetDirectoryName(FilePath); }
        }

        private string _imgSize;
        public string ImgSize
        {
            get { return _imgSize; }
        }


        public ImgHash(int hashSideSize = 16)
        {
            _hashSide = hashSideSize;

            _hashData = new bool[hashSideSize * hashSideSize];
        }

        /// <summary>
        /// Method to compare 2 image hashes
        /// </summary>
        /// <returns>% of similarity</returns>
        public double CompareWith(ImgHash compareWith)
        {
            if (HashData.Length != compareWith.HashData.Length)
            {
                throw new Exception("Cannot compare hashes with different sizes");
            }

            int differenceCounter = 0;

            for (int i = 0; i < HashData.Length; i++)
            {
                if (HashData[i] != compareWith.HashData[i])
                {
                    differenceCounter++;
                }
            }
            
            return 100 - differenceCounter/100.0 * HashData.Length / 2.0;
        }

        public void GenerateFromPath(string path)
        {
            FilePath = path;

            Bitmap image = (Bitmap)Image.FromFile(path, true);

            _imgSize = $"{image.Size.Width}x{image.Size.Height}";

            GenerateFromImage(image);

            image.Dispose();
        }

        private void GenerateFromImage(Bitmap img)
        {
            List<bool> lResult = new List<bool>();

            //resize img to 16x16px (by default) or with configured size 
            Bitmap bmpMin = new Bitmap(img, new Size(_hashSide, _hashSide));
            
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                {
                    //reduce colors to true and false
                    lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                }
            }

            _hashData = lResult.ToArray();

            bmpMin.Dispose();
        }
    }
}
