# ImgComparator

Here is realized simple idea to comare images to find similar.

It's generate 256 bit image hash: Change img size to 16x16px(by default, but you can change size) and reduce colors to black/white (which equals true/false in this console output)

This give you ability to find duplicates/similar images.

Lets try to compare the following imgs:

![Alt text](http://image.prntscr.com/image/9f02f5e2cd2b4f469e06a7c1621d2a67.png "Algorithm to compare two images in C#")


And you will got the following hash:

![Alt text](http://image.prntscr.com/image/56e01d77a2f54318b3d46e4a2038540d.png "Algorithm to compare two images in C#")


Examples of usage:
```
var comparator = new ImgComparator();

comparator.AddPicFolderByPath(somePathToSmeDir);
            
var _comparationResult = comparator.FindDuplicatesWithTollerance(80);
```

as result you will recieve ```List<List<ImgHash>>```

```
int counter = 1;
foreach (var hashBlock in _comparationResult)
{
    Console.WriteLine($"Duplicates {counter++} Group:");

    foreach (var singleHash in hashBlock)
    {
        Console.WriteLine(singleHash.FilePath);
    }
}
```

This code will print all similar/duplicated images in selected path.
* subfolders included
* all imgs with the following extensions: ".bmp", ".gif", ".exif", ".jpg", ".jpeg", ".png", ".tiff"
