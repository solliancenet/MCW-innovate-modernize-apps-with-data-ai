import {IMetadataDto} from "../interfaces/IMetadataDto";
import {IMachineTelemetryDto} from "../interfaces/IMachineTelemetryDto";

export class QueryService {
	static async getMetadataAsync(): Promise<IMetadataDto> {
		const response = await fetch("/api/metadata");
		return response.json();
	}
	
	static async getMachineTelemetryAsync(): Promise<IMachineTelemetryDto[]> {
		const response = await fetch("/api/machineTelemetry");
		return response.json();
	}
}