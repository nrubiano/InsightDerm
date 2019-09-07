export class Entity {
    json() {
        return JSON.stringify(this);
    };
}

export enum Roles {
    Specialist = 'specialist',
    Admin = 'admin',
    Doctor = 'doctor'
}
