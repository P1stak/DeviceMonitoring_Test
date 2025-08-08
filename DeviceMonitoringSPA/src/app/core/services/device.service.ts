import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';

interface ApiDevice {
  Id: string;
  Name: string;
  StartTime: string;
  EndTime: string;
  Version: string;
}

interface Device {
  id: string;
  name: string;
  startTime: string;
  endTime: string;
  version: string;
}

@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  private apiUrl = 'https://localhost:7086/api/Device';
  private devicesSubject = new BehaviorSubject<Device[]>([]);
  private selectedDeviceSubject = new BehaviorSubject<Device | null>(null);
  
  devices$ = this.devicesSubject.asObservable();
  selectedDevice$ = this.selectedDeviceSubject.asObservable();
  devices: Device[] = [];

  constructor(private http: HttpClient) {
    this.loadDevices();
  }

  loadDevices(): void {
    this.http.get<ApiDevice[]>(this.apiUrl)
      .pipe(
        map((apiDevices: ApiDevice[]) => {
          return apiDevices.map(device => ({
            id: device.Id,
            name: device.Name,
            startTime: device.StartTime,
            endTime: device.EndTime,
            version: device.Version
          }));
        })
      )
      .subscribe({
        next: (devices: Device[]) => {
          this.devices = devices;
          this.devicesSubject.next(devices);
        },
        error: (error) => {
          console.error('Ошибка загрузки:', error);
        }
      });
  }

  selectDevice(device: Device | null): void {
    this.selectedDeviceSubject.next(device);
  }

  calculateDuration(start: string, end: string): string {
    const startDate = new Date(start);
    const endDate = new Date(end);
    const diff = endDate.getTime() - startDate.getTime();
    const hours = Math.floor(diff / (1000 * 60 * 60));
    const minutes = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60));
    return `${hours} ч ${minutes} мин`;
  }

clearOldRecords(cutoffDate: Date): void {
  const confirmDelete = confirm(`Удалить записи старше ${cutoffDate.toLocaleString()}?`);
  if (!confirmDelete) return;

  const dateString = cutoffDate.toISOString();

  this.http.request('DELETE', `${this.apiUrl}/clear-old-records`, {
    body: `"${dateString}"`,
    headers: { 
      'Content-Type': 'application/json'
    }
  }).subscribe({
    next: () => {
      alert('Удаление выполнено успешно!');
      this.loadDevices();
    },
    error: (err) => {
      console.error('Full error:', err);
      alert(`Ошибка ${err.status}. Проверьте консоль для деталей.`);
    }
  });
}

  createBackup(fileName: string = 'backup.json'): void {

    this.http.post<{message: string}>(`${this.apiUrl}/backup`, `"${fileName}"`, {
      headers: { 'Content-Type': 'application/json' }
    }).subscribe({
      next: (response) => {
        alert(response.message || `Резервная копия создана: ${fileName}`);
      },
      error: (err) => {
        console.error('Ошибка создания бэкапа:', err);
        alert(err.error?.message || 'Ошибка при создании резервной копии');
      }
    });
  }
}