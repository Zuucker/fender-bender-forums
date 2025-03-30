<template>
	<div class="layout">
		<nav
			class="navbar d-flex justify-content-between bg-blazy"
			style="height: 8vh">
			<div>logo</div>
			<div>search</div>
			<div>actions</div>
		</nav>
		<div class="mid-section d-flex">
			<div
				class="side-menu d-flex flex-column align-items-center col-3 bg-blight"
				style="height: 92vh">
				<div class="side-menu-select col-10">car type</div>
				<div class="side-menu-select col-10">history</div>
				<div class="side-menu-select col-10">DIY</div>
				<div class="side-menu-select col-10">MarketPlace</div>
				<div class="side-menu-select col-10">Race results</div>
			</div>
			<div class="content col-9 bg-blight" style="height: 92vh">
				<RouterView />
			</div>
		</div>
	</div>
</template>
