import { Entity } from "./base";

export class MedicalBackground extends Entity {
    personal: string;
    pathological: string;
    surgical: string;
    alergic: string;
    pharmacological: string;
    transfusions: string;
    hereditary: string;
    toxic: string;
    other: string;
}