import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { DeviceListComponent } from './device-list/device-list.component';

@NgModule({
  declarations: [
    DeviceListComponent
  ],
  imports: [
    CommonModule,
    MatProgressBarModule,
    MatListModule,
    MatIconModule,
    MatButtonModule
  ],
  exports: [
    DeviceListComponent
  ]
})
export class DeviceModule { }