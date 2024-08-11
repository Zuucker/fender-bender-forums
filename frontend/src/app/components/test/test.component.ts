import { Component, inject, Input } from '@angular/core';
import { NgFor, NgIf } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'test',
  standalone: true,
  imports: [HttpClientModule, NgIf, NgFor],
  templateUrl: './test.component.html',
  styleUrl: './test.component.css',
})
export class TestComponent {
  @Input() url: string = '';
  @Input() num: number = 0;
  dataArray: any[] = [];

  httpClient = inject(HttpClient);
  ngOnInit(): void {
    this.httpClient.get(this.url).subscribe((data) => {
      // console.log(data);
      if (Array.isArray(data)) this.dataArray = data;
    });
  }

  getFirstPropertyValue(item: any): any {
    const key = Object.keys(item)[this.num];
    return item[key];
  }
}
