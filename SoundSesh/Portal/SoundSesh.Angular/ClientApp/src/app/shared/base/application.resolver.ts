import { BaseService } from '../services/base-service';

export class ApplicationResolver {
  public static getConfiguration(): any {
    let application: string;
    if (document.location.pathname.indexOf('Musicians') !== -1) {
      application = 'Musicians';
    } else if (document.location.pathname.indexOf('Studios') !== -1) {
      application = 'Studios';
    } else {
      application = 'SoundSesh';
    }
    return application.charAt(0).toUpperCase() + application.slice(1);
  }
}
