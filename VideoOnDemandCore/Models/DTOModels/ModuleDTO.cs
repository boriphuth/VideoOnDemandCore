﻿using System.Collections.Generic;

namespace VideoOnDemandCore.Models.DTOModels
{
    public class ModuleDTO
    {
        public int Id { get; set; }
        public string ModuleTitle { get; set; }
        public List<VideoDTO> Videos { get; set; }
        public List<DownloadDTO> Downloads { get; set; }
    }
}
