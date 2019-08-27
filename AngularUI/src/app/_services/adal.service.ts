import { Injectable, Inject } from '@angular/core';

import * as lib from 'adal-angular';
import * as AuthenticationContext from 'adal-angular';

import { AdalConfig } from '../_helpers/adal.config';

@Injectable({
  providedIn: 'root'
})
export class AdalService {

  private context: AuthenticationContext;

  constructor(@Inject('adalConfig') private adalConfig: AdalConfig) {
    if (typeof adalConfig === 'function') {
      this.adalConfig = adalConfig;
    }
    this.context = lib.inject(this.adalConfig);
    this.handleCallback();
   }

  public login() {
    this.context.login();
  }

  public logout() {
    this.context.logOut();
  }

  private handleCallback() {
    const isCallback = this.context.isCallback(window.location.hash);
    if (!isCallback) {
      this.context.handleWindowCallback();
    }
  }
}
