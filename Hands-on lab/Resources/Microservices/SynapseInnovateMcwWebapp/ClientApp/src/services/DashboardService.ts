export class DashboardService {
	public static async getDashboardUrlAsync(): Promise<string> {
		const response = await fetch("/api/powerBiDashboard");
		
		if (!response.ok) {
			throw new Error(`Fetching the Dashboard URL failed with code ${response.status}: ${response.statusText}`);
		}
		
		return response.text();
	}
}