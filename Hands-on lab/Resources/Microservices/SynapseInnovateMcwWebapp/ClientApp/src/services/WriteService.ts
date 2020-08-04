import {IMetadataFactory} from "../interfaces/IMetadataFactory";
import {IMetadataMachine} from "../interfaces/IMetadataMachine";
import {IMetadataMaintenanceLookup} from "../interfaces/IMetadataMaintenanceLookup";

export class WriteService {
	public static async createMetadataFactoryAsync(metadataFactory: IMetadataFactory): Promise<void> {
		const response = await fetch("/api/metadata/factory", {
			method: "post", body: JSON.stringify(metadataFactory), headers: {
				'Accept': 'application/json',
				'Content-Type': 'application/json'
			}
		});

		if (!response.ok) {
			this.catchError(response);
		}
	}

	public static async createMetadataMachineAsync(metadataMachine: IMetadataMachine): Promise<void> {
		const response = await fetch("/api/metadata/machine", {
			method: "post", body: JSON.stringify(metadataMachine), headers: {
				'Accept': 'application/json',
				'Content-Type': 'application/json'
			}
		});
		
		if (!response.ok) {
			this.catchError(response);
		}
	}
	
	public static async createMaintenanceLookupAsync(metadataMaintenanceLookup: IMetadataMaintenanceLookup): Promise<void> {
		const response = await fetch("/api/metadata/maintenanceLookup", {
			method: "post", body: JSON.stringify(metadataMaintenanceLookup), headers: {
				'Accept': 'application/json',
				'Content-Type': 'application/json'
			}
		});
		
		if (!response.ok) {
			this.catchError(response);
		}
	}
	
	private static catchError(response: Response) {
		throw new Error(`Encountered an error with code ${response.status}: ${response.statusText}`);
	}
}