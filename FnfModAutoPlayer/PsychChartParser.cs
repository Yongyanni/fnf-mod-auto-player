using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FnfModAutoPlayer
{
    public static class PsychChartParser
    {
        public static List<NoteInfo> LoadPlayerNotes(string path)
        {
            var json = File.ReadAllText(path);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var root = JsonSerializer.Deserialize<PsychChart>(json, options);
            if (root == null || root.notes == null)
                throw new Exception("谱面解析失败：notes 为空");

            var result = new List<NoteInfo>();

            foreach (var section in root.notes)
            {
                if (section.sectionNotes == null)
                    continue;

                // 只打玩家部分
                if (!section.mustHitSection)
                    continue;

                foreach (var raw in section.sectionNotes)
                {
                    if (raw.Length < 3) continue;

                    double time = raw[0].GetDouble();
                    int dir = raw[1].GetInt32();
                    double len = raw[2].GetDouble();

                    result.Add(new NoteInfo
                    {
                        Time = time,
                        Direction = dir,
                        Length = len
                    });
                }

            }

            return result.OrderBy(n => n.Time).ToList();
        }
    }
}
