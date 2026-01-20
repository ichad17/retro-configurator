import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order, CreateOrderRequest } from '../models/order.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ConfiguratorService {
  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl || 'https://localhost:5001/api';

  createOrder(request: CreateOrderRequest): Observable<Order> {
    return this.http.post<Order>(`${this.apiUrl}/orders`, request);
  }

  getOrder(id: string): Observable<Order> {
    return this.http.get<Order>(`${this.apiUrl}/orders/${id}`);
  }

  getOrdersByEmail(email: string): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}/orders/customer/${email}`);
  }

  completeOrder(id: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/orders/${id}/complete`, {});
  }

  cancelOrder(id: string): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/orders/${id}/cancel`, {});
  }

  calculatePrice(
    consoleType: number,
    numberOfControllers: number,
    hdmiSupport: boolean,
    customColor: boolean
  ): number {
    const basePrices: { [key: number]: number } = {
      1: 199.99, // NES
      2: 249.99, // SNES
      3: 229.99, // Genesis
      4: 299.99, // N64
      5: 349.99  // PlayStation
    };

    const basePrice = basePrices[consoleType] || 0;
    const colorPremium = customColor ? 29.99 : 0;
    const controllersCost = numberOfControllers * 39.99;
    const hdmiCost = hdmiSupport ? 49.99 : 0;

    return basePrice + colorPremium + controllersCost + hdmiCost;
  }
}
