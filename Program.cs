using System;
using System.IO;

namespace mvtcs
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (var path in args)
                {
                    PrintMvtFileContents(path);
                }
            }
            else
            {
                PrintMvtFileContents("tile.mvt");
            }
        }

        static void PrintMvtFileContents(string pathToFile)
        {
			Tile tile;
			using (var input = File.OpenRead(pathToFile))
			{
				tile = Tile.Parser.ParseFrom(input);
			}

			int numberOfLayers = tile.Layers.Count;
			Console.WriteLine("Parsed a tile with {0} layers!", numberOfLayers);

			foreach (var layer in tile.Layers)
			{
				Console.WriteLine("Layer name: {0}", layer.Name);
				foreach (var feature in layer.Features)
				{
					Console.WriteLine("  Feature with id: {0}", feature.Id);
					var tags = feature.Tags;
					for (int i = 0; i < tags.Count; i += 2)
					{
						var key = layer.Keys[(int)tags[i]];
						var val = layer.Values[(int)tags[i + 1]];
						Console.WriteLine("    {0} = {1}", key, val);
					}

					Console.WriteLine("    Type: {0}", feature.Type);
				}
			}
        }
    }
}
