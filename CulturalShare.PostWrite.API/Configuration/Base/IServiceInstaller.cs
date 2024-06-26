﻿using Serilog.Core;

namespace CulturalShare.PostWrite.API.Configuration.Base;

public interface IServiceInstaller
{
    void Install(WebApplicationBuilder builder, Logger logger);
}
