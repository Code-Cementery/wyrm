/*
        string GetPath(string fn)
        {
            string dbPath = "";

            if (Application.platform == RuntimePlatform.Android)
            {
                // Android
                string oriPath = Path.Combine(Application.streamingAssetsPath, fn);

                // Android only use WWW to read file
                WWW reader = new WWW(oriPath);
                while (!reader.isDone) { }

                var realPath = Application.persistentDataPath + "/db";
                System.IO.File.WriteAllBytes(realPath, reader.bytes);

                dbPath = realPath;
            }
            else
            {
                // iOS
                dbPath = Path.Combine(Application.streamingAssetsPath, fn);
            }

            return dbPath;
        }
*/