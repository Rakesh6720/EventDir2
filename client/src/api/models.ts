export interface Organizer {
    id: number;
    username: string;
    email: string;
}

export interface Member {
    id: number;
    username: string;
    email: string;
}

export interface Enrollment {
    id: number;
    member: Member;
}

export interface Event {
    id: number;
    name: string;
    address1: string;
    address2: string;
    city: string;
    state: string;
    zip: string;
    description: string;
    imageURL: string;
    organizer: Organizer;
    enrollments: Enrollment[];
}
