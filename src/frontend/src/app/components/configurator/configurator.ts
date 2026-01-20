import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ConfiguratorService } from '../../services/configurator';
import { ConsoleType } from '../../models/console-config.model';
import { CreateOrderRequest } from '../../models/order.model';

@Component({
  selector: 'app-configurator',
  imports: [CommonModule, FormsModule],
  templateUrl: './configurator.html',
  styleUrl: './configurator.css',
})
export class ConfiguratorComponent {
  private configuratorService = inject(ConfiguratorService);

  consoleTypes = [
    { value: ConsoleType.NES, name: 'NES' },
    { value: ConsoleType.SNES, name: 'SNES' },
    { value: ConsoleType.Genesis, name: 'Genesis' },
    { value: ConsoleType.N64, name: 'N64' },
    { value: ConsoleType.PlayStation, name: 'PlayStation' }
  ];

  selectedConsole: ConsoleType = ConsoleType.NES;
  numberOfControllers: number = 2;
  hdmiSupport: boolean = false;
  customColor: boolean = false;
  colorHex: string = '#000000';
  customerEmail: string = '';
  calculatedPrice: number = 0;
  orderCreated: boolean = false;
  orderId: string = '';
  error: string = '';

  ngOnInit() {
    this.updatePrice();
  }

  updatePrice() {
    this.calculatedPrice = this.configuratorService.calculatePrice(
      this.selectedConsole,
      this.numberOfControllers,
      this.hdmiSupport,
      this.customColor
    );
  }

  createOrder() {
    this.error = '';
    this.orderCreated = false;

    if (!this.customerEmail) {
      this.error = 'Please enter your email address';
      return;
    }

    const request: CreateOrderRequest = {
      consoleType: this.selectedConsole,
      numberOfControllers: this.numberOfControllers,
      hdmiSupport: this.hdmiSupport,
      customColor: this.customColor,
      colorHex: this.customColor ? this.colorHex : undefined,
      customerEmail: this.customerEmail
    };

    this.configuratorService.createOrder(request).subscribe({
      next: (order) => {
        this.orderId = order.id;
        this.orderCreated = true;
      },
      error: (err) => {
        this.error = err.error?.error || 'Failed to create order. Please try again.';
      }
    });
  }
}
