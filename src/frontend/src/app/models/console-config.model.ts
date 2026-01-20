export enum ConsoleType {
  NES = 1,
  SNES = 2,
  Genesis = 3,
  N64 = 4,
  PlayStation = 5
}

export interface ConsoleConfig {
  consoleType: ConsoleType;
  numberOfControllers: number;
  hdmiSupport: boolean;
  customColor: boolean;
  colorHex?: string;
}
