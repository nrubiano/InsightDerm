import { Entity } from "./base";

export class Patient extends Entity {
    id: string;
    name: string;
    identificationType:string;
    identificationNumber: string;
    bornDate: any;
    occupation: string;
    address: string;
    phone: string;
    mobilePhone: string;
    mail: string;
    genre: string;
    maritalStatusId:string;
}