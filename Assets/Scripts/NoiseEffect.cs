using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class NoiseRenderFeature : ScriptableRendererFeature
{
    class NoisePass : ScriptableRenderPass
    {
        public Material material;
        private RenderTargetHandle temporaryColorTexture;

        public NoisePass(Material mat)
        {
            material = mat;
            temporaryColorTexture.Init("_TempColorTexture");
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (material == null) return;

            CommandBuffer cmd = CommandBufferPool.Get("NoiseEffect");

            // ✅ Pegar a câmera target dentro do Execute, sem usar a variável da classe
            var cameraTarget = renderingData.cameraData.renderer.cameraColorTarget;

            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            cmd.GetTemporaryRT(temporaryColorTexture.id, opaqueDesc);

            Blit(cmd, cameraTarget, temporaryColorTexture.Identifier(), material);
            Blit(cmd, temporaryColorTexture.Identifier(), cameraTarget);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    public Material noiseMaterial;
    private NoisePass noisePass;

    public override void Create()
    {
        noisePass = new NoisePass(noiseMaterial);
        noisePass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        // ✅ Não acessa cameraColorTarget aqui, só enfileira o pass
        renderer.EnqueuePass(noisePass);
    }
}
