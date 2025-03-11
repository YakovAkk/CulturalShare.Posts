﻿using Serilog.Core;

namespace CulturalShare.PostRead.API.Configuration.Base;

public interface IServiceInstaller
{
    void Install(WebApplicationBuilder builder, Logger logger);
}
