import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { bootstrapApplication } from '@angular/platform-browser';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { NZ_I18N, en_US } from 'ng-zorro-antd/i18n';

import { AppComponent } from './app/app.component';

const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideAnimationsAsync(),
    { provide: NZ_I18N, useValue: en_US }
  ]
};

bootstrapApplication(AppComponent, appConfig).catch((error: unknown) => {
  console.error(error);
});
