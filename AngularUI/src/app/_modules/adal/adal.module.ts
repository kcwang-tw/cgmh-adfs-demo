import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModuleWithProviders } from '@angular/compiler/src/core';

import { AdalService } from 'src/app/_services/adal.service';
import { AdalConfig } from 'src/app/_helpers/adal.config';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class AdalModule {
  static forRoot(adalConfig: AdalConfig): ModuleWithProviders {
    return {
      ngModule: AdalModule,
      providers: [AdalService, { provide: 'adalConfig', useValue: adalConfig }]
    };
  }
 }
