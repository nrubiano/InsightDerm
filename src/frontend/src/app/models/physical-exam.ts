import { Entity } from "./base";

export class PhysicalExam extends Entity {
    ta: number;
    fc: number;
    weight: number;
    size: number;
    temperature: number;
    comments: string;
}