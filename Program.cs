string path = @"c:\Users\Admin\Downloads\30da91a108";

var filesSizeBeforeClear = FileSizes(path);
Console.WriteLine($"Исходный размер папки: {filesSizeBeforeClear} байт");

var clearResult = ClearAllFilesDirs(path);

var filesSizeAfterClear = FileSizes(path);
Console.WriteLine($"Освобождено: {filesSizeBeforeClear - filesSizeAfterClear} байт");
Console.WriteLine($"Текущий размер папки: {filesSizeAfterClear} байт");




long FileSizes(string path)
{
    DirectoryInfo di = new DirectoryInfo(path);
    long fileSize = 0;

    try
    {
        if (di.Exists)
        {
            var allFilesPaths = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            foreach (var filePath in allFilesPaths)
            {
                if (File.Exists(filePath))
                {
                    var file = new FileInfo(filePath);

                    fileSize += file.Length;
                }
                else
                    Console.WriteLine($"Некорректный путь файла '{filePath}'!");
            }
        }
        else
            Console.WriteLine("Некорректный путь!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка! {ex.Message}");
    }

    return fileSize;
}

bool ClearAllFilesDirs(string path)
{
    DirectoryInfo di = new DirectoryInfo(path);
    bool result = false;

    try
    {
        if (di.Exists)
        {
            var allFilesPaths = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            foreach (var filePath in allFilesPaths)
            {
                if (File.Exists(filePath))
                {
                    var file = new FileInfo(filePath);

                    if ((DateTime.Now - file.LastWriteTime).TotalMinutes > 30)
                        file.Delete();
                }
                else
                    Console.WriteLine($"Некорректный путь файла '{filePath}'!");
            }

            var allDirsPaths = Directory.GetDirectories(path, "*.*", SearchOption.AllDirectories);
            foreach (var dirPath in allDirsPaths)
            {
                if (Directory.Exists(dirPath))
                {
                    var dir = new DirectoryInfo(dirPath);

                    if ((DateTime.Now - dir.LastWriteTime).TotalMinutes > 30)
                        dir.Delete();
                }
                else
                    Console.WriteLine($"Некорректный путь внутреннего каталога '{dirPath}'!");
            }

            result = true;
        }
        else
            Console.WriteLine($"Некорректный путь каталога '{path}'!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка! {ex.Message}");

        result = false;
    }

    return result;
}