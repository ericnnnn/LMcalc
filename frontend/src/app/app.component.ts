import { HttpClient } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';
import { firstValueFrom } from 'rxjs';
import { environment } from '../environments/environment';


declare global { interface Window { __API_BASE__?: string } }

interface Weather { city: string; temp: number; }

@Component({
  selector: 'app-root',
  // imports: [RouterOutlet],
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {

  // apiBase = new URL(window.__API_BASE__ ?? '/api', window.location.origin);
  apiBase = environment.apiUrl;
  title = 'frontend';
  weather = signal<{city:string,temp:number}[]>([]);
  loading = signal(false);
  error = signal<string | null>(null);

  private http = inject(HttpClient);
  
  // show spinner at least this long
  private readonly minSpinnerMs = 500;

  async loadWeather() {

    this.loading.set(true);
    this.error.set(null);
    const started = performance.now();
    
    try {    
      const data = await firstValueFrom(this.http.get<Weather[]>(`${this.apiBase}/weatherforecast`));
      this.weather.set(data);
    } catch(e:any){
      this.error.set(e.message ?? 'Failed');
    } finally {
      const elapsed = performance.now() - started;
      const remain = Math.max(0, this.minSpinnerMs - elapsed);
      setTimeout(() => this.loading.set(false), remain);
    }
  }
}
