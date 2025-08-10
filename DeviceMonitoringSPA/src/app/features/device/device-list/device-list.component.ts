import { Component } from '@angular/core';
import { DeviceService } from '../../../core/services/device.service';


@Component({
  selector: 'app-device-list',
  templateUrl: './device-list.component.html',
  styleUrls: ['./device-list.component.less']
})
export class DeviceListComponent {
  devices$ = this.deviceService.devices$;
  constructor(private deviceService: DeviceService) {}

  clearOldRecords(): void {
    const cutoffDate = new Date();
    cutoffDate.setMonth(cutoffDate.getMonth() - 1);
    this.deviceService.clearOldRecords(cutoffDate);
  }
}