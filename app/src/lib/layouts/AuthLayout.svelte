<script>
  import Footer from "$lib/components/Footer.svelte";
</script>

<div id="layoutAuthentication">
    <div id="layoutAuthentication_content">
        <main>
            <div class="container">
                <div class="row justify-content-center">
                    <slot />
                </div>
            </div>
        </main>
    </div>
    <div id="layoutAuthentication_footer">
        <Footer />
    </div>
</div>
