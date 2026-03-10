using System.Collections.Generic;
using System.Text.Json;

namespace FnfModAutoPlayer
{
    public class PsychChart
    {
        public List<PsychSection> notes { get; set; }
        public double speed { get; set; }
        public string song { get; set; }
    }

    public class PsychSection
    {
        public JsonElement[][] sectionNotes { get; set; }

        public bool mustHitSection { get; set; }
    }
}
