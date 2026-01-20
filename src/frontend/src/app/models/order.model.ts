import { ConsoleConfig } from './console-config.model';

export enum OrderStatus {
  Pending = 1,
  Processing = 2,
  Completed = 3,
  Cancelled = 4
}

export interface Order {
  id: string;
  configuration: ConsoleConfig;
  totalPrice: number;
  status: OrderStatus;
  createdAt: Date;
  completedAt?: Date;
  customerEmail: string;
}

export interface CreateOrderRequest {
  consoleType: number;
  numberOfControllers: number;
  hdmiSupport: boolean;
  customColor: boolean;
  colorHex?: string;
  customerEmail: string;
}
