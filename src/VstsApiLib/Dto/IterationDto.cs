using System;
using System.Collections.Generic;

namespace Devpro.VstsClient.VstsApiLib.Dto
{
    public class IterationFindResultDto
    {
        public int count { get; set; }
        public List<IterationValueDto> value { get; set; }
    }

    public class IterationValueDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public IterationAttributesDto attributes { get; set; }
        public string url { get; set; }
    }

    public class IterationAttributesDto
    {
        public DateTime? startDate { get; set; }
        public DateTime? finishDate { get; set; }
        public string timeFrame { get; set; }
    }
}
