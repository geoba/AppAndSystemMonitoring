﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringService {
  public partial class MonitoringService: ServiceBase {
    public MonitoringService() {
      InitializeComponent();
    }

    protected override void OnStart( string[] args ) {
    }

    protected override void OnStop() {
    }
  }
}
