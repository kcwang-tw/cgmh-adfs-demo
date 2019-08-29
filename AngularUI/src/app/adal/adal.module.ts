import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdalService } from './adal.service';



@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ]
})
export class AdalModule {
  static forRoot(config: any): ModuleWithProviders {
    return {
      ngModule: AdalModule,
      providers: [
        AdalService,
        { provide: 'adalConfig', useValue: config }
      ]
    };
  }
 }
