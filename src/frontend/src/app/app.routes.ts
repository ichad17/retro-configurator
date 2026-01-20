import { Routes } from '@angular/router';
import { ConfiguratorComponent } from './components/configurator/configurator';

export const routes: Routes = [
  { path: '', component: ConfiguratorComponent },
  { path: '**', redirectTo: '' }
];
