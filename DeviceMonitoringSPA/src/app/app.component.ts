import { Component } from '@angular/core';
import { DeviceService } from './core/services/device.service';
import { Device } from './core/models/device.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.less']
})
export class AppComponent {
  cutoffDate: string = new Date().toISOString().split('T')[0];
  backupFileName: string = 'backup.json';
  searchQuery: string = '';

  constructor(public deviceService: DeviceService) {}

  get filteredDevices(): Device[] {
    if (!this.deviceService.devices) return [];
    
    if (!this.searchQuery.trim()) return this.deviceService.devices;

    const query = this.searchQuery.toLowerCase();
    return this.deviceService.devices.filter((device: Device) => 
      device.name.toLowerCase().includes(query) || 
      device.id.toLowerCase().includes(query) ||
      device.version.toLowerCase().includes(query)
    );
  }

  get activeDevicesCount(): number {
    return this.deviceService.devices?.filter((device: Device) => 
      this.isDeviceActive(device)).length || 0;
  }

  isDeviceActive(device: Device): boolean {
    if (!device?.endTime) return true;
    return new Date(device.endTime) > new Date();
  }

  calculateDuration(start: string, end: string): string {
    const startDate = new Date(start);
    const endDate = end ? new Date(end) : new Date();
    const diff = endDate.getTime() - startDate.getTime();
    const hours = Math.floor(diff / (1000 * 60 * 60));
    const minutes = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60));
    return `${hours} ч ${minutes} мин`;
  }

  refreshDevices(): void {
    this.deviceService.loadDevices();
  }
  
  clearOldRecords(): void {
    if (!this.cutoffDate) {
      alert('Пожалуйста, выберите дату');
      return;
    }
    this.deviceService.clearOldRecords(new Date(this.cutoffDate));
  }

  createBackup(): void {
    const fileName = this.backupFileName.trim() || 'backup.json';
    this.deviceService.createBackup(fileName);
  }
}