import React from "react";

export const PowerBiDashboard: React.FC<{url: string | null}> = ({url}) => {
	return (
		<>
			{url === null
				? <span>Loading...</span>
				: <iframe width="1140" height="541.25"
					src={url}
					frameBorder="0" allowFullScreen={true} />}
		</>
	)
}