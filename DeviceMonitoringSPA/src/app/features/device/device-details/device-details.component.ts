import { Component, Input } from '@angular/core';
import { Device } from '../../../core/models/device.model';

@Component({
  selector: 'app-device-details',
  templateUrl: './device-details.component.html',
  styleUrls: ['./device-details.component.less']
})
export class DeviceDetailsComponent {
  @Input() device: Device | null = null;

  formatDate(date: Date | string | undefined): string {
    if (!date) return 'N/A';
    return new Date(date).toLocaleString();
  }

  calculateDuration(start: Date | string | undefined, end: Date | string | undefined): string {
    if (!start || !end) return 'N/A';
    
    const startDate = new Date(start);
    const endDate = new Date(end);
    const diff = endDate.getTime() - startDate.getTime();
    
    const hours = Math.floor(diff / (1000 * 60 * 60));
    const minutes = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60));
    
    return `${hours}h ${minutes}m`;
  }
}