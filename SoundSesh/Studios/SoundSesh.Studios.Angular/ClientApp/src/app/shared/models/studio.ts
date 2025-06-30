import { BusinessHour } from './business-hour';
import { SocialMedia } from './social-media';
import { StudioFileInformation } from 'src/app/feature/studios/studio.file';
import { Room } from './room';

export class Studio {
    id: number;
    email: string;
    phonenumber: string;
    address1: string;
    address2: string;
    city: string;
    state: string;
    zipcode: string;
    profileimage: string;
    bannerimage: string;
    name: string;
    about: string;
    rules: string;
    cancellationpolicy: string;
    userid: string;
    websitelink: string;
    audiocliplink: string;
    skilllevel: string;
    defaultprofileimage: string;
    defaultbannerimage: string;

    genres: string[] = [];
    amenities: string[] = [];

    hoursofoperation: BusinessHour[] = [];
    socialmedia: SocialMedia[] = [];
    applicationfiles: StudioFileInformation[] = [];
    activeapplicationfiles: StudioFileInformation[] = [];
    activerooms: Room[] = [];
    rooms: Room[] = [];

}

export class StudioCardDecks {

    constructor(studios: Studio[]) {
        this.cards = studios;
        this.count = studios.length;
    }
    public cards: Studio[];
    public count: number;
}
