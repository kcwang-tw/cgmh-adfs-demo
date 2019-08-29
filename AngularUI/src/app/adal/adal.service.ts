import { Injectable, Inject } from '@angular/core';

import * as AuthenticationContext from 'adal-angular';

@Injectable({
  providedIn: 'root'
})

export class AdalService {

  private context: AuthenticationContext;

  constructor(@Inject('adalConfig') private config: any) {
    this.context = new AuthenticationContext(this.config);
    this.handleCallback();
  }

  login() {
    this.context.login();
  }

  logOut() {
    this.context.logOut();
  }

  handleCallback() {
    const isCallback = this.context.isCallback(window.location.hash);

    if (isCallback) {
      this.context.handleWindowCallback();
    }
  }

  get accessToken() {
    return this.context.getCachedToken(this.config.clientId);
  }

  get userInfo() {
    return this.context.getCachedUser();
  }

  get isAuthenticated() {
    return (this.accessToken && this.userInfo);
  }

  get userName() {
    if (this.isAuthenticated) {
      return this.userInfo.userName;
    }

    return '';
  }
}
