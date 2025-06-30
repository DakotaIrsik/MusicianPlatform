import { SocialMedia } from './social-media';
import { MusicianFileInformation } from 'src/app/feature/Musicians/Musician.file';

export class Musician {
    id: number;
    address1: string;
    address2: string;
    city: string;
    state: string;
    zipcode: string;
    profileimage: string;
    bannerimage: string;
    firstname: string;
    lastname: string;
    about: string;
    rules: string;
    cancellationpolicy: string;
    userid: string;
    discography: string;

    crafts: string[] = [];
    defaultprofileimage: string;
    defaultbannerimage: string;

    genres: string[] = [];
    socialmedia: SocialMedia[] = [];
    applicationfiles: MusicianFileInformation[] = [];
    activeapplicationfiles: MusicianFileInformation[] = [];
}
